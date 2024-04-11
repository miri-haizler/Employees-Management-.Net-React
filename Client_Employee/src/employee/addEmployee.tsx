
import React, { Fragment, useEffect, useState } from "react";
import { json, useLocation, useNavigate } from "react-router-dom";
import { Controller, useFieldArray, useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import axios from "axios";
import SendIcon from "@mui/icons-material/Send";
import Role from "../entities/Role";
import { Button, TextField, MenuItem, FormControl, InputLabel, Select, Checkbox, FormControlLabel, Box } from "@mui/material";

const schema = yup.object({
  firstName: yup.string()
    .required("First name is required")
    .min(2, "First name should be at least 2 characters"),
  lastName: yup.string()
    .required("Last name is required")
    .min(2, "Last name should be at least 2 characters"),
  tz: yup.string()
    .required("Tz is required")
    .matches(/^\d{9}$/, "Tz should be a 9-digit number"),
  startDate: yup.date().required("Start date is required"),
  dateOfBirth: yup.date().required("Date of birth is required"),
  gender: yup.number().required("Gender is required"),
  isActive: yup.bool().required("Is active status is required"),
  rolesEmployee: yup.array()
    .of(
      yup.object({
        roleId: yup.number().required("Role name is required"),
        startDateOfJob: yup.date().required("Start date of job is required"),
        isManagerial: yup.bool().required("Managerial status is required"),
        isActive: yup.bool().required("Is active status is required"),
      })
    ).required("Roles are required"),
}).required();
const AddEmployee = () => {

  const { state } = useLocation();
  const navigate = useNavigate();
  const [roles, setRoles] = useState<Role[]>([]);
  const [rolesNotInEmployee, setRolesOnEmployee] = useState([])

  const { register, control, handleSubmit, formState: { errors }, watch } = useForm({
    resolver: yupResolver(schema),
    defaultValues: state
  });

  const { fields: RolesEmployee, append: appendRolesEmployee } = useFieldArray({
    control,
    name: "rolesEmployee"
  });

  const watchFields = watch(['rolesEmployee'])

  useEffect(() => {
    const data = watchFields[0];
    if (JSON.stringify(data) != JSON.stringify(rolesNotInEmployee)) {
      setRolesOnEmployee(data)
    }
  }, [watchFields])

  useEffect(() => {
    axios
      .get("https://localhost:7117/api/Role")
      .then((res) => {
        setRoles(res.data);
        setRolesOnEmployee(res.data)
      })
      .catch((error) => {
        console.error("Error fetching roles:", error);
      });
  }, []);

  const onSubmit = (data) => {
    if (state) {
      axios
        .put(`https://localhost:7117/api/Employee/${state.tz}`, data)
        .then((res) => {
          navigate("/employees");
        })
        .catch((error) => {
          console.error("Error edited employee:", error);
        });
    } else {
      axios
        .post("https://localhost:7117/api/Employee", data)
        .then((res) => {
          navigate("/employees");
        })
        .catch((error) => {
          console.error("Error adding employee:", error);
        });
    }
  };

  return (

    <form id="form" onSubmit={handleSubmit(onSubmit)}>;

      <Box display="flex" flexDirection="column" alignItems="center" textAlign="center">
        <div>
          <label>FirstName</label>
          <TextField
            {...register("firstName")}
            placeholder="First Name"
          />
          {errors.firstName ? errors.firstName.message?.toString() : ""}
        </div>

        <div>
          <label>LastName</label>
          <TextField
            {...register("lastName")}
            placeholder="LastName"
          />
          {errors.lastName ? errors.lastName.message?.toString() : ""}
        </div>

        <div>
          <label>Tz</label>
          <TextField
            {...register("tz")}
            placeholder="Tz"
          />
          {errors.tz ? errors.tz.message?.toString() : ""}
        </div>

        <div>
          <label>DateOfBirth</label>
          <TextField
            {...register("dateOfBirth")}
            placeholder="DateOfBirth"
            type="datetime-local"
          />
          {errors.dateOfBirth ? errors.dateOfBirth.message?.toString() : ""}
        </div>

        <div>
          <label>StartDate</label>
          <TextField
            {...register("startDate")}
            placeholder="StartDate"
            type="datetime-local"
          />
          {errors.startDate ? errors.startDate.message?.toString() : ""}
        </div>
        <div>
          <FormControl>
            <InputLabel id="gender-label">Gender</InputLabel>
            <Controller
              name={`gender`}
              control={control}
              render={({ field }) => (<Select
                {...field}
                labelId="gender-label"
              >
                <MenuItem value="0">Male</MenuItem>
                <MenuItem value="1">Female</MenuItem>
              </Select>)} />

          </FormControl>
          {errors.gender && <p>{errors.gender.message?.toString()}</p>}
        </div>
        <FormControlLabel control={<Checkbox {...register("isActive")} defaultChecked={state?.isActive} />} label="Is Active" />
        <div>
          <label>Roles</label>
          <br />

          {RolesEmployee?.map((item, index) => {
            return (

              <Fragment key={index}>

                <hr />
                <Controller
                  name={`rolesEmployee.${index}.roleId`}
                  control={control}
                  render={({ field }) => (<Select
                    {...field}
                    labelId="role-label"
                  >
                    {roles?.map((r, i) => (
                      <MenuItem key={r.name} value={r.id} disabled={rolesNotInEmployee.some(x => x['roleId'] == r.id)}>{r.name}</MenuItem>
                    ))}

                  </Select>)} />

                <TextField
                  {...register(`rolesEmployee.${index}.startDateOfJob`)}
                  placeholder="Enter Start Date"
                  type="datetime"
                >
                </TextField>
                <FormControlLabel
                  control={<Controller
                    name={`rolesEmployee.${index}.isManagerial`}
                    control={control}
                    render={({ field: props }) => (
                      <Checkbox
                        {...props}
                        checked={props.value}
                        onChange={(e) => props.onChange(e.target.checked)} />
                    )} />}
                  label="Is Managerial" />
                <FormControlLabel
                  control={<Controller
                    name={`rolesEmployee.${index}.isActive`}
                    control={control}
                    render={({ field: props }) => (
                      <Checkbox
                        {...props}
                        checked={props.value}
                        onChange={(e) => props.onChange(e.target.checked)} />
                    )} />}
                  label="Is Active" />
              </Fragment>

            );
          })}

        </div>
        <Button
          onClick={() => appendRolesEmployee({})}
        >
          Add Role
        </Button>
        <br />
        <Button type="submit" variant="contained" endIcon={<SendIcon />}>
          Send
        </Button>
      </Box >
    </form >
  );
};
export default AddEmployee;
