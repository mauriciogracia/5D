import React, { useState, useEffect } from "react";
import {
    TextField,
    Button,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
} from "@mui/material";
import { addPermission, getPermissionTypes } from "../apiBroker";

function AddPermissionForm({ handleClose, onPermissionAdded }) {
    const [permission, setPermission] = useState({
        NombreEmpleado: "",
        ApellidoEmpleado: "",
        TipoPermisoId: 1, // Default to the first permission type
        FechaPermiso: new Date(),
    });
    const [error, setError] = useState("");
    const [permissionTypes, setPermissionTypes] = useState([]);

    useEffect(() => {
        getPermissionTypes(setPermissionTypes, setError);
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log("1111");
        addPermission(permission, setError)
            .then(() => {
                console.log("2222");
                onPermissionAdded(); // Call the refresh function
                console.log("3333");
                handleClose();
                console.log("4444");
            })
            .catch((error) => {
                console.error(error);
            });
    };

    return (
        <form onSubmit={handleSubmit}>
            <TextField
                style={{ marginBottom: "16px" }}
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
                style={{ marginBottom: "16px" }}
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
            <Button
                style={{ marginTop: "16px" }}
                type="submit"
                variant="contained"
                color="primary"
            >
                SAVE
            </Button>
            {error && <div className="error-message">{error}</div>}
        </form>
    );
}

export default AddPermissionForm;
