import React, { useState, useEffect } from "react";
import { Grid } from "@mui/material";
import { Link } from "react-router-dom";
import { getPermissionTypes } from "../apiBroker";

function PermissionTypesGridView({ history }) {
    const [data, setData] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        getPermissionTypes(setData, setError);
    }, []);

    return (
        <div>
            <h3>PERMISSION TYPES</h3>
            <Link to="/">HOME</Link>
            <br></br>
            <Grid container spacing={2}>
                {/* Header row */}
                <Grid item xs={12}>
                    <Grid container spacing={2} direction="row">
                        <Grid item xs={6}>
                            <strong>Id</strong>
                        </Grid>
                        <Grid item xs={6}>
                            <strong>Descripcion</strong>
                        </Grid>
                    </Grid>
                </Grid>
                {data.map((item) => (
                    <Grid item xs={12} key={item.id}>
                        <Grid container spacing={2} direction="row">
                            <Grid item xs={6}>
                                {item.id}
                            </Grid>
                            <Grid item xs={6}>
                                {item.descripcion}
                            </Grid>
                        </Grid>
                    </Grid>
                ))}
            </Grid>
        </div>
    );
}

export default PermissionTypesGridView;
