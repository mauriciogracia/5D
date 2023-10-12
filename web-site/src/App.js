// App.js
import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

import AddPermission from "./permissions/add.jsx"; // Import your component

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/add-permission" component={AddPermission} />
                {/* Add other routes as needed */}
            </Routes>
        </Router>
    );
}

export default App;
