class ApiConfig {
    constructor() {
        this.apiBaseUrl = "http://webapi:5000";
        this.GetPermissionTypesEndpoint = `${this.apiBaseUrl}/PermissionsType/GetPermissionTypes`;
        this.AddPermissionEndpoint = `${this.apiBaseUrl}/api/addPermission`;
    }
}
const apiConfig = new ApiConfig();

export default apiConfig;
