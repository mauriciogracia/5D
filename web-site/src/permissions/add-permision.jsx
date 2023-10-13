// PermissionForm.js
import React, { useState, useEffect } from "react";
import ApiConfig from "../apiConfig";

function PermissionForm() {
    const [permission, setPermission] = useState("");
    const [error, setError] = useState("");
    const [permissionTypes, setPermissionTypes] = useState([]);

    useEffect(() => {
        console.log(
            "GetPermissionTypesURL:" + ApiConfig.PermissionTypesEndpoint
        );

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
            })
            .catch((err) => {
                console.log(err.message);
                setError(err.message);
            });
    }, []); // The empty dependency array ensures this effect runs only once

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            // Make an API call to add the permission
            const response = await fetch(ApiConfig.PermissionEndpoint, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ permission }),
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
            <form id="permissionForm">
                <label htmlFor="NombreEmpleado">Nombre del Empleado:</label>
                <input
                    type="text"
                    id="NombreEmpleado"
                    name="NombreEmpleado"
                    required
                />
                <br />
                <label htmlFor="ApellidoEmpleado">Apellido del Empleado:</label>
                <input
                    type="text"
                    id="ApellidoEmpleado"
                    name="ApellidoEmpleado"
                    required
                />
                <br />
                <label htmlFor="TipoPermiso">Tipo de Permiso:</label>
                <select id="TipoPermiso" name="TipoPermiso" required>
                    {permissionTypes.map((type) => (
                        <option key={type.id} value={type.id}>
                            {type.name}
                        </option>
                    ))}
                </select>
                <br />
                <label htmlFor="FechaPermiso">Fecha del Permiso:</label>
                <input
                    type="date"
                    id="FechaPermiso"
                    name="FechaPermiso"
                    required
                />
                <br />
                <button type="submit">Add Permission</button>
            </form>
        </div>
    );
}

export default PermissionForm;
