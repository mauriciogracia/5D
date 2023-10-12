// PermissionForm.js
import React, { useState } from "react";

function PermissionForm() {
    const [permission, setPermission] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            // Make an API call to add the permission
            const response = await fetch("/api/addPermission", {
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
                    <option value="1">Tipo 1</option>
                    <option value="2">Tipo 2</option>
                    <option value="3">Tipo 3</option>
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
            In this modified form: NombreEmpleado and ApellidoEmpleado are text
            input fields. TipoPermiso is a dropdown (select) input with options
            representing different types of permissions. FechaPermiso is a date
            input field for selecting the date of the permission. You should
            adjust the options in the TipoPermiso select field to match the
            possible types of permissions defined in your system. The JavaScript
            code for handling the form submission should also be updated to
            collect the values of these input fields and send them to your
            server when making the API call to add a new permission.
        </div>
    );
}

export default PermissionForm;
