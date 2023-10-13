import React, { useState, useEffect } from "react";
import ApiConfig from "../apiConfig";

function PermissionForm() {
    const [permission, setPermission] = useState({
        NombreEmpleado: "",
        ApellidoEmpleado: "",
        TipoPermisoId: "",
        FechaPermiso: new Date(),
    });
    const [error, setError] = useState("");
    const [permissionTypes, setPermissionTypes] = useState([]);
    const [selectedPermissionType, setSelectedPermissionType] = useState("");

    useEffect(() => {
        // Fetch permission types from the API endpoint when the component mounts
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
                //The combo has preselected the first PermissionType
                setSelectedPermissionType(1);
            })
            .catch((err) => {
                console.log(err.message);
                setError(err.message);
            });
    }, []);

    const handlePermissionTypeChange = (e) => {
        setSelectedPermissionType(e.target.value);
        // Update the selected permission type in the permission state
        setPermission({
            ...permission,
            TipoPermisoId: e.target.value,
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Set FechaPermiso to the current date
        permission.FechaPermiso = new Date();

        // Get the selected TipoPermisoId value from the dropdown
        permission.TipoPermisoId = selectedPermissionType;

        try {
            // Make an API call to add the permission
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
        <div>
            {error && <div className="error-message">{error}</div>}
            <form id="permissionForm" onSubmit={handleSubmit}>
                <label htmlFor="NombreEmpleado">Nombre del Empleado:</label>
                <input
                    type="text"
                    id="NombreEmpleado"
                    name="NombreEmpleado"
                    required
                    value={permission.NombreEmpleado}
                    onChange={(e) =>
                        setPermission({
                            ...permission,
                            NombreEmpleado: e.target.value,
                        })
                    }
                />
                <br />
                <label htmlFor="ApellidoEmpleado">Apellido del Empleado:</label>
                <input
                    type="text"
                    id="ApellidoEmpleado"
                    name="ApellidoEmpleado"
                    required
                    value={permission.ApellidoEmpleado}
                    onChange={(e) =>
                        setPermission({
                            ...permission,
                            ApellidoEmpleado: e.target.value,
                        })
                    }
                />
                <br />
                <label htmlFor="TipoPermisoId">Tipo de Permiso:</label>
                <select
                    id="TipoPermisoId"
                    name="TipoPermisoId"
                    value={selectedPermissionType}
                    onChange={handlePermissionTypeChange}
                    required
                >
                    {permissionTypes.map((type) => (
                        <option key={type.id} value={type.id}>
                            {type.descripcion}
                        </option>
                    ))}
                </select>
                <br />
                <button type="submit">Add Permission</button>
            </form>
        </div>
    );
}

export default PermissionForm;
