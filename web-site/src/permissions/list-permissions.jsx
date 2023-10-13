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
            <h3>EMPLOYEE PERMISSIONS</h3>
            <Button
                style={{ marginBottom: "16px" }}
                variant="contained"
                color="primary"
                onClick={() => history.push("/add-permission")}
            >
                NEW PERMISSION
            </Button>
            <Grid container spacing={2}>
                {/* Header row */}
                <Grid item xs={12}>
                    <Grid container spacing={2} direction="row">
                        <Grid item xs={4}>
                            <strong>Nombre</strong>
                        </Grid>
                        <Grid item xs={4}>
                            <strong>Apellido</strong>
                        </Grid>
                        <Grid item xs={4}>
                            <strong>Permiso</strong>
                        </Grid>
                    </Grid>
                </Grid>
                {data.map((item) => (
                    <Grid item xs={12} key={item.id}>
                        <Grid container spacing={2} direction="row">
                            <Grid item xs={4}>
                                {item.nombreEmpleado}
                            </Grid>

                            <Grid item xs={4}>
                                {item.apellidoEmpleado}
                            </Grid>

                            <Grid item xs={4}>
                                {item.tipoPermisoId}
                            </Grid>
                        </Grid>
                    </Grid>
                ))}
            </Grid>
        </div>
    );
}

export default PermissionGridView;
