import React, { useState, useEffect } from "react";
import {
    Grid,
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
} from "@mui/material";

import { Link } from "react-router-dom";
import { getPermissions } from "../apiBroker";
import AddPermissionForm from "./add-permission";

function PermissionGridView({ history }) {
    const [data, setData] = useState([]);
    const [error, setError] = useState(null);
    const [isDialogOpen, setIsDialogOpen] = useState(false); // State to manage the dialog open state

    useEffect(() => {
        getPermissions(setData, setError);
    }, []);

    const openDialog = () => {
        setIsDialogOpen(true);
    };

    const closeDialog = () => {
        setIsDialogOpen(false);
    };

    const refreshPermissions = () => {
        console.log("refreshPermissions") ;
        getPermissions(setData, setError);
        closeDialog(); // Close the dialog after refreshing
    };

    return (
        <div>
            <h3>EMPLOYEE PERMISSIONS</h3>
            <Link to="/">HOME</Link>
            <br></br>
            <Button
                style={{ marginBottom: "16px" }}
                variant="contained"
                color="primary"
                onClick={openDialog} // Open the dialog on button click
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

            {/* Render the PermissionForm within a Dialog */}
            <Dialog open={isDialogOpen} onClose={closeDialog}>
                <DialogTitle>Add New Permission</DialogTitle>
                <DialogContent>
                    <AddPermissionForm
                        handleClose={closeDialog}
                        onPermissionAdded={refreshPermissions}
                    />
                </DialogContent>
            </Dialog>
        </div>
    );
}

export default PermissionGridView;
