import React from "react";
import { BrowserRouter as Router, Route, Routes, Link } from "react-router-dom";

// Import your components
import AddPermission from "./permissions/add-permision.jsx";

function Home() {
    return (
        <div>
            <h1>HOME PAGE</h1>
            <Link to="/add-permission">Go to Add Permission</Link>
        </div>
    );
}

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/add-permission" element={<AddPermission />} />
            </Routes>
        </Router>
    );
}

export default App;
