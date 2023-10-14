import React, { useState, useEffect } from "react";
import { Grid } from "@mui/material";
import ApiConfig from "../apiConfig";
import axios from "axios";

function PermissionTypesGridView({ history }) {
    const [data, setData] = useState([]);

    useEffect(() => {
        fetchPermissionsList();
    }, []);

    const fetchPermissionsList = () => {
        console.log("LISTING TYPES");
        // TODO Fetch data from the API endpoint
        axios
            .get(ApiConfig.PermissionTypesEndpoint)
            .then((response) => {
                console.log("Data:", response.data);
                setData(response.data);
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    };

    return (
        <div>
            <h3>PERMISSION TYPES</h3>
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
