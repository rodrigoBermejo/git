using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.DirectoryServices.Protocols;
using System.Net;

namespace CRM.Application.Helpers
{
    public class LdapService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LdapService> _logger;

        public LdapService(IConfiguration configuration, ILogger<LdapService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                var ldapPath = _configuration["ActiveDirectory:LdapPath"];
                var adminUsername = _configuration["ActiveDirectory:Username"];
                var adminPassword = _configuration["ActiveDirectory:Password"];

                _logger.LogInformation("Connecting to LDAP server at {LdapPath} with admin user {AdminUsername}", ldapPath, adminUsername);

                using (var ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(ldapPath)))
                {
                    ldapConnection.Credential = new NetworkCredential(adminUsername, adminPassword);
                    ldapConnection.Bind(); // Bind with admin credentials

                    _logger.LogInformation("Successfully bound to LDAP server with admin credentials");

                    var searchRequest = new SearchRequest(
                        "dc=isc,dc=upa,dc=edu,dc=mx", // Base DN
                        $"(sAMAccountName={username})", // LDAP filter
                        SearchScope.Subtree,
                        null);

                    var searchResponse = (SearchResponse)ldapConnection.SendRequest(searchRequest);

                    if (searchResponse.Entries.Count == 1)
                    {
                        var userDn = searchResponse.Entries[0].DistinguishedName;
                        _logger.LogInformation("User {Username} found with distinguished name {UserDn}", username, userDn);

                        ldapConnection.Credential = new NetworkCredential(username, password);
                        ldapConnection.Bind(); // Bind with user credentials

                        _logger.LogInformation("Successfully bound to LDAP server with user credentials");
                        return true;
                    }
                    else
                    {
                        _logger.LogWarning("User {Username} not found in LDAP", username);
                        return false;
                    }
                }
            }
            catch (LdapException ex)
            {
                _logger.LogError(ex, "LDAP error occurred while validating user {Username}", username);
                throw; // Re-throw the exception to get more details
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while validating user {Username}", username);
                throw; // Re-throw the exception to get more details
            }
        }
    }
}