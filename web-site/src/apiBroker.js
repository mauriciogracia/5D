import axios from "axios";
import ApiConfig from "./apiConfig";

const getPermissionTypes = (setData, setError) => {
    console.log("LISTING TYPES");
    axios
        .get(ApiConfig.PermissionTypesEndpoint)
        .then((response) => {
            console.log("Data:", response.data);
            setData(response.data);
        })
        .catch((error) => {
            console.error("Error:", error);
            setError(error);
        });
};

const getPermissions = (setData, setError) => {
    console.log("LISTING PERMISSIONS");
    axios
        .get(ApiConfig.PermissionEndpoint)
        .then((response) => {
            console.log(response.data);
            setData(response.data);
        })
        .catch((error) => {
            console.error(error);
            setError(error);
        });
};

const addPermission = (permission, setError) => {
    console.log("ADD PERMISSION");
    // Set FechaPermiso to the current date
    permission.FechaPermiso = new Date();

    axios
        .post(ApiConfig.PermissionEndpoint, permission)
        .then((response) => {
            console.log(response.data);
        })
        .catch((error) => {
            console.error(error);
            setError(error);
        });
};

export { getPermissionTypes, getPermissions, addPermission };
