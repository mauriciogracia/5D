import React, { useState, useEffect } from "react";
import { Grid, Button } from "@mui/material";
import { Link } from "react-router-dom";
import { getPermissions } from "../apiBroker";

function PermissionGridView({ history }) {
    const [data, setData] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        getPermissions(setData, setError);
    }, []);

    return (
        <div>
            <h3>EMPLOYEE PERMISSIONS</h3>
            <Link to="/">HOME</Link>
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
                        <Grid item xs={3}>
                            <strong>Id</strong>
                        </Grid>
                        <Grid item xs={3}>
                            <strong>Nombre</strong>
                        </Grid>
                        <Grid item xs={3}>
                            <strong>Apellido</strong>
                        </Grid>
                        <Grid item xs={3}>
                            <strong>Permiso</strong>
                        </Grid>
                    </Grid>
                </Grid>
                {data.map((item) => (
                    <Grid item xs={12} key={item.id}>
                        <Grid container spacing={2} direction="row">
                            <Grid item xs={3}>
                                {item.id}
                            </Grid>
                            <Grid item xs={3}>
                                {item.nombreEmpleado}
                            </Grid>
                            <Grid item xs={3}>
                                {item.apellidoEmpleado}
                            </Grid>
                            <Grid item xs={3}>
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
