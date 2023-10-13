import React, { useState, useEffect } from "react";
import { Grid, Button } from "@mui/material";
import ApiConfig from "../apiConfig";

function PermissionGridView({ history }) {
    const [data, setData] = useState([]);

    useEffect(() => {
        fetchPermissionsList();
    }, []);

    const fetchPermissionsList = () => {
        console.log("LISTING");
        // Fetch data from the API endpoint
        fetch(ApiConfig.PermissionEndpoint)
            .then((response) => {
                if (!response.ok) {
                    console.log(response);
                    throw new Error("Failed to fetch data");
                }
                return response.json();
            })
            .then((data) => {
                setData(data);
            })
            .catch((error) => {
                console.error(error);
            });
    };

    return (
        <div>
            <Button
                variant="contained"
                color="primary"
                onClick={() => history.push("/add-permission")}
            >
                ADD PERMISSION
            </Button>
            <Grid container spacing={2}>
                {data.map((item) => (
                    <Grid item xs={12} sm={6} md={4} lg={3} key={item.id}>
                        {/* Render the grid items for each permission */}
                        <div>
                            <strong>Nombre del Empleado:</strong>{" "}
                            {item.NombreEmpleado}
                        </div>
                        <div>
                            <strong>Apellido del Empleado:</strong>{" "}
                            {item.ApellidoEmpleado}
                        </div>
                        <div>
                            <strong>Tipo de Permiso:</strong> {item.TipoPermiso}
                        </div>
                        {/* Add more information as needed */}
                    </Grid>
                ))}
            </Grid>
        </div>
    );
}

export default PermissionGridView;
