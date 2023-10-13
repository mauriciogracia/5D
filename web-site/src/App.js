import React from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";

// Import your components
import AddPermission from "./permissions/add-permision.jsx";
import PermissionGridView from "./permissions/list-permissions.jsx";

function Home() {
    return (
        <div>
            <h1>5D - Permission Managment</h1>
            <Link to="/add-permission">Add Permission</Link>
            <br></br>
            <Link to="/list-permissions">List Permissions</Link>
        </div>
    );
}

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
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
