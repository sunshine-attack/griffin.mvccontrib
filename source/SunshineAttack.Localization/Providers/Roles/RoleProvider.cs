﻿using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SunshineAttack.Localization.Providers.Roles
{
    /// <summary>
    /// Provides roles through a repository
    /// </summary>
    /// <remarks>
    /// You need to register a  <see cref="IRoleRepository"/> in your inversion of control container. This classes
    /// uses <see cref="DependencyResolver"/> to find it's dependencies.
    /// </remarks>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// public class MvcApplication : System.Web.HttpApplication
    /// {
    ///     protected void Application_Start()
    ///     {
    ///         _unity.RegisterType<IRoleRepository, RavenDbRepository>();
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        /// <summary>
        /// Gets repository used to retrieve information from the data source.
        /// </summary>
        private IRoleRepository Repository
        {
            get { return DependencyResolver.Current.GetService<IRoleRepository>(); }
        }

        #region Overrides of RoleProvider

        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        /// <returns>
        /// The name of the application to store and retrieve role information for.
        /// </returns>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
        ///   
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> on a provider after the provider has already been initialized.</exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            name = string.IsNullOrEmpty(name) ? "RoleProvider" : name;

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Griffin.MvcContrib Role Provider");
            }

            base.Initialize(name, config);

            if ((config["ApplicationName"] == null) || String.IsNullOrEmpty(config["ApplicationName"]))
            {
                ApplicationName = HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                ApplicationName = config["ApplicationName"];
            }
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="username">The user name to search for.</param><param name="roleName">The role to search in.</param>
        public override bool IsUserInRole(string username, string roleName)
        {
            var user = Repository.GetUser(ApplicationName, username);
            return user.IsInRole(roleName);
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <param name="username">The user to return a list of roles for.</param>
        public override string[] GetRolesForUser(string username)
        {
            var user = Repository.GetUser(ApplicationName, username);
            if (user == null)
                return new string[0];
            return user.Roles.ToArray();
        }

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            if (RoleExists(roleName))
                throw new ProviderException("Role '" + roleName + "' already exists.");

            if (roleName.Contains(","))
                throw new ArgumentNullException("roleName", "Role names cannot contain commas.");

            Repository.CreateRole(ApplicationName, roleName);
        }

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        /// <param name="roleName">The name of the role to delete.</param><param name="throwOnPopulatedRole">If true, throw an exception if <paramref name="roleName"/> has one or more members and do not delete <paramref name="roleName"/>.</param>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (!RoleExists(roleName))
                throw new ProviderException("Role '" + roleName + "' do not exists.");

            var count = Repository.GetNumberOfUsersInRole(ApplicationName, roleName);
            if (count != 0)
                throw new ProviderException("Role '" + roleName + "' have assigned users.");

            Repository.RemoveRole(ApplicationName, roleName);
            return true;
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        public override bool RoleExists(string roleName)
        {
            return Repository.Exists(ApplicationName, roleName);
        }

        /// <summary>
        /// Adds the specified user names to the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be added to the specified roles. </param><param name="roleNames">A string array of the role names to add the specified user names to.</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var roleName in roleNames.Where(roleName => !RoleExists(roleName)))
            {
                throw new ProviderException("Role '" + roleName + "' do not exist.");
            }

            foreach (var username in usernames.Where(username => username.Contains(",")))
            {
                throw new ProviderException("User '" + username + "' contains commas.");
            }


            foreach (var roleName in roleNames)
            {
                foreach (var username in usernames)
                {
                    Repository.AddUserToRole(ApplicationName, roleName, username);
                }
            }
        }

        /// <summary>
        /// Removes the specified user names from the specified roles for the configured applicationName.
        /// </summary>
        /// <param name="usernames">A string array of user names to be removed from the specified roles. </param><param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                foreach (var username in usernames)
                {
                    Repository.RemoveUserFromRole(ApplicationName, roleName, username);
                }
            }
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role for the configured applicationName.
        /// </returns>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        public override string[] GetUsersInRole(string roleName)
        {
            return Repository.GetUsersInRole(ApplicationName, roleName).ToArray();
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            return Repository.GetRoleNames(ApplicationName).ToArray();
        }

        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch"/> and the user is a member of the specified role.
        /// </returns>
        /// <param name="roleName">The role to search in.</param><param name="usernameToMatch">The user name to search for.</param>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            var userName = usernameToMatch.ToLower();
            return Repository.FindUsersInRole(ApplicationName, roleName, userName).ToArray();
        }

        #endregion
    }
}