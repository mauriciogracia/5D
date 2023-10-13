import React, { useState, useEffect } from "react";
import {
    TextField,
    Button,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
} from "@mui/material";
import ApiConfig from "../apiConfig";

function PermissionForm() {
    const [permission, setPermission] = useState({
        NombreEmpleado: "",
        ApellidoEmpleado: "",
        TipoPermisoId: 1, // Default to the first permission type
        FechaPermiso: new Date(),
    });
    const [error, setError] = useState("");
    const [permissionTypes, setPermissionTypes] = useState([]);

    useEffect(() => {
        fetchPermissionTypes();
    }, []);

    const fetchPermissionTypes = () => {
        fetch(ApiConfig.PermissionTypesEndpoint)
            .then((response) => {
                if (!response.ok) {
                    console.log(response);
                    throw new Error("Failed to fetch permission types");
                }
                return response.json();
            })
            .then((data) => {
                setPermissionTypes(data);
            })
            .catch((err) => {
                setError(err.message);
            });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        // Set FechaPermiso to the current date
        permission.FechaPermiso = new Date();
        try {
            const response = await fetch(ApiConfig.PermissionEndpoint, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(permission),
            });

            if (!response.ok) {
                throw new Error("Failed to add permission");
            }

            // Handle success
            // Reset the form or show a success message
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <TextField
                label="Nombre del Empleado"
                variant="outlined"
                fullWidth
                required
                value={permission.NombreEmpleado}
                onChange={(e) =>
                    setPermission({
                        ...permission,
                        NombreEmpleado: e.target.value,
                    })
                }
            />
            <TextField
                label="Apellido del Empleado"
                variant="outlined"
                fullWidth
                required
                value={permission.ApellidoEmpleado}
                onChange={(e) =>
                    setPermission({
                        ...permission,
                        ApellidoEmpleado: e.target.value,
                    })
                }
            />
            <FormControl fullWidth variant="outlined">
                <InputLabel htmlFor="TipoPermisoId">Tipo de Permiso</InputLabel>
                <Select
                    labelId="permission-type-label"
                    id="TipoPermisoId"
                    value={permission.TipoPermisoId}
                    onChange={(e) =>
                        setPermission({
                            ...permission,
                            TipoPermisoId: e.target.value,
                        })
                    }
                    label="Tipo de Permiso"
                >
                    {permissionTypes.map((type) => (
                        <MenuItem key={type.id} value={type.id}>
                            {type.descripcion}
                        </MenuItem>
                    ))}
                </Select>
            </FormControl>
            <Button type="submit" variant="contained" color="primary">
                Add Permission
            </Button>
            {error && <div className="error-message">{error}</div>}
        </form>
    );
}

export default PermissionForm;
