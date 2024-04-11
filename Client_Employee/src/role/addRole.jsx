import { Button, Dialog, DialogActions, DialogContent, DialogContentText } from '@mui/material';
import { useForm } from "react-hook-form";
import axios from "axios";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';


const AddRoleDialog = () => {
    const [open, setOpen] = useState(true);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        navigate('/employees')
    };

    const navigate = useNavigate();

    const schema = yup.object({
        Name: yup.string().required()
    }).required();

    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(schema)
    });

    const onSubmit = (data) => {
        try {
            axios.get("https://localhost:7117/api/Role")
                .then(res => {
                    const existingRoles = res.data;
                    const roleExists = existingRoles.some(role => role.name === data.Name);
                    if (roleExists) {
                        alert("Role already exists!");
                    } else {
                        axios.post("https://localhost:7117/api/Role", data)
                            .then(res => {
                                handleClose();
                                navigate('/employees')
                            }).catch(error => {
                                console.error(error);
                            });

                    }
                })
        } catch (error) {
            console.error(error);
        }
    };


    return (
        <div>
            {/* <Button variant="outlined" onClick={handleClickOpen}>
        Add Role
      </Button> */}
            <Dialog open={open} onClose={handleClose}>
                {/* <DialogTitle>Add Role</DialogTitle> */}
                <DialogContent>
                    <DialogContentText>
                        To add a new role, please enter the name below.
                    </DialogContentText>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <input type="text" {...register("Name")} placeholder="Enter Role Name" />
                        <p>{errors.Name?.message}</p>
                        <Button type="submit">Submit</Button>
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
};

export default AddRoleDialog;
