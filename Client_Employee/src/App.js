import { Route, Routes, useNavigate } from 'react-router-dom';
import './App.css';
import ShowEmployees from './employee/showAllEmployees.tsx';
import DeleteEmployee from './employee/deleteEmployee.tsx';
import AddEmployee from './employee/addEmployee.tsx';
import { Button } from '@mui/material';
import AddRole from './role/addRole.jsx';

function App() {

  const navigate = useNavigate();

  return (
    <>
      <div style={{ textAlign: 'center' }}>
        <Button onClick={() => navigate(`/addEmployee`,{ state: null })}>Add Employee</Button>
        <Button onClick={() => navigate(`/employees`)} style={{ marginLeft: '20px' }}>Show Employees</Button>
        <Button onClick={() => navigate(`/addRole`)}  style={{ marginLeft: '20px' }}>Add Role</Button>
      </div>

      <Routes>
        <Route path='/' element={<ShowEmployees />} />
        <Route path="/employees" element={<ShowEmployees />} />
        <Route path="/deleteEmployee" element={<DeleteEmployee />} />
        <Route path="/addEmployee" element={<AddEmployee />} />
        <Route path="/addRole" element={<AddRole />} />
      </Routes>
      
    </>
  );

}
export default App;