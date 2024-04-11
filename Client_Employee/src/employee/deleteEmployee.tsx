import axios from "axios";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";

const DeleteEmployee = () => {
    const { state } = useLocation();
   
    const [delemployee, setdelemployee] = useState<string>("")
    const navigate = useNavigate();
    useEffect(() => {
        axios.delete(`https://localhost:7117/api/Employee/${state}`)
            .then(res => {
                navigate('/employees')
            })
            .catch(error => {
                console.error("Error deleting employee:", error);
            });

    }, []);
    return null;
}
export default DeleteEmployee;