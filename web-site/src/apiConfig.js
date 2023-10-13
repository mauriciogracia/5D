//const apiBaseUrl = "http://webapi:5000"; when in production
const apiBaseUrl = "http://localhost:5000";
const PermissionEndpoint = `${apiBaseUrl}/Permissions`;
const PermissionTypesEndpoint = `${apiBaseUrl}/PermissionsType`;

export default {
    apiBaseUrl,
    PermissionEndpoint,
    PermissionTypesEndpoint,
};
