using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace WebAPI.BackendServer.IdentityServer
{
	public class Config
	{
		public static IEnumerable<IdentityResource> Ids =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};
		public static IEnumerable<ApiResource> Apis =>
			new List<ApiResource>
			{
				new ApiResource("api.webapi", "Web API")
			};
		public static IEnumerable<Client> Clients =>
			new Client[] {
				new Client{
					ClientId = "webportal",
					ClientSecrets = { new Secret("secret".Sha256()) },
					AllowedGrantTypes = GrantTypes.Code,
					RequireConsent = false,
					RequirePkce = true,
					AllowOfflineAccess = true,
					RedirectUris = { "https://localhost:7196/sigin-oidc" },
					PostLogoutRedirectUris = { "https://localhost:7196/signout-callback-oidc" },
					AllowedScopes = {
					 IdentityServerConstants.StandardScopes.OpenId,
					 IdentityServerConstants.StandardScopes.Profile,
					 IdentityServerConstants.StandardScopes.OfflineAccess,
					 "api.webapi"
					}
				},
					new Client{
						ClientId = "swagger",
						ClientName = "Swagger Client",

						AllowedGrantTypes = GrantTypes.Implicit,
						AllowAccessTokensViaBrowser = true,
						RequireConsent = false,

						RedirectUris = { "https://localhost:7196/swagger/oauth2-redirect.html" },
						PostLogoutRedirectUris = { "https://localhost:7196/swagger/oauth2-redirect.html" },
						AllowedCorsOrigins = { "https://localhost:7196" },

						AllowedScopes = new List<string>
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							"api.webapi"
						}
					},
					new Client
					{
						ClientName = "Angular Admin",
						ClientId = "angular_admin",
						AllowedGrantTypes = GrantTypes.Code,
						AccessTokenType = AccessTokenType.Jwt,
						RequireConsent = false,

						RequireClientSecret = false,
						RequirePkce = true,
						AllowAccessTokensViaBrowser = true,
						RedirectUris = new List<string>
						{
							"https://localhost:4200",
							"https://localhost:4200/signin-callback",
							"https://localhost:4200/assets/silent-callback.html"
						},
						PostLogoutRedirectUris = new List<string>
						{
							"https://localhost:4200/unauthorized",
							"https://localhost:4200/signout-callback",
							"https://localhost:4200"
						},
						AllowedCorsOrigins = new List<string>
						{
							"https://localhost:4200"
						},
						AllowedScopes = new List<string>
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							"api.webapi"
						},
					}
				};
	};
}

