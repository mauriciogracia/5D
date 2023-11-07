import React from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";

// Import components
import AddPermission from "./permissions/add-permission.jsx";
import PermissionGridView from "./permissions/list-permissions.jsx";
import PermissionTypesGridView from "./permissions-types/list-permissions-types.jsx";

function Home() {
    return (
        <div>
            <h1>5D - Permission Managment - v7.11</h1>
            <Link to="/list-permission-types">Permissions Types</Link>
            <br></br>
            <Link to="/add-permission">Add Permission</Link>
            <br></br>
            <Link to="/list-permissions">Employee Permissions</Link>
        </div>
    );
}

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route
                    path="/list-permission-types"
                    element={<PermissionTypesGridView />}
                />
                <Route path="/add-permission" element={<AddPermission />} />
                <Route
                    path="/list-permissions"
                    element={<PermissionGridView />}
                />
            </Routes>
        </Router>
    );
}

export default App;
