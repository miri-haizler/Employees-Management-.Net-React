import React, { Fragment, useEffect, useState } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { useNavigate } from 'react-router-dom';
import axios from "axios";
import * as XLSX from 'xlsx';
import { Button, CircularProgress, Grid, IconButton, InputBase, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import Employee from "../entities/Employee";
import CloudDownloadIcon from '@mui/icons-material/CloudDownload';
import SearchIcon from '@mui/icons-material/Search';

const ShowEmployees = () => {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [filter, setFilter] = useState<string>("");
  const navigate = useNavigate();
  let filteredEmployees: Employee[];

  useEffect(() => {
    axios.get("https://localhost:7117/api/Employee")
      .then((res) => {
        setEmployees(res.data);
      })
      .catch(error => {
        console.error("Error show employees:", error);
      });
  }, []);

  filteredEmployees = employees.filter(employee =>
    employee.firstName && employee.firstName.toLowerCase().includes(filter.toLowerCase()) ||
    employee.lastName && employee.lastName.toLowerCase().includes(filter.toLowerCase()) ||
    employee.tz && employee.tz.toLowerCase().includes(filter.toLowerCase()) ||
    employee.startDate && employee.startDate.toDateString
  );

  const handleFilterChange = (event) => {
    setFilter(event.target.value);
  };

  const convertToJavaScriptDate = (dateTimeString: Date): Date => {
    const jsDate = new Date(dateTimeString.toString());
    return jsDate;
  };

  const exportEmployeesToExcel = () => {
    const worksheet = XLSX.utils.json_to_sheet(employees);
    const workbook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    const excelBuffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'buffer' });
    const data = new Blob([excelBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    const url = URL.createObjectURL(data);
    const link = document.createElement('a');
    link.href = url;
    link.download = 'employees.xlsx';
    link.click();
  };

  const editForm = (tz) => {
    axios.get(`https://localhost:7117/api/Employee/${tz}`).then(res => {
      navigate(`/addEmployee`, { state: res.data })
    }).catch(error => {
      console.error('Error edit employee:', error);
    });
  }

  return (
    <form className="MuiFormControlLabel-asterisk">
      {employees.length > 0 ? (
        <Fragment>
          <Grid container justifyContent="center" spacing={2}>
            <Grid item>
              <Paper component="form" sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: 300 }}>

                <InputBase
                  sx={{ ml: 1, flex: 1 }}
                  placeholder="Search..."
                  inputProps={{ 'aria-label': 'Search...' }}
                  onChange={handleFilterChange}
                />
                <IconButton type="button" sx={{ p: '10px' }} aria-label="search">
                  <SearchIcon />
                </IconButton>
              </Paper>
            </Grid>
            <Grid item>
              <Button
                onClick={exportEmployeesToExcel}
                variant="contained"
                sx={{
                  bgcolor: '#2196f3',
                  color: '#ffffff',
                  '&:hover': {
                    bgcolor: '#1976d2'
                  },
                  '& .MuiButton-startIcon': {
                    marginRight: '8px',
                  },
                }}
                startIcon={<CloudDownloadIcon />}
              >
                Export to Excel
              </Button>
            </Grid>
          </Grid>
          <br></br>
          <Grid container justifyContent="center">
            <Grid item xs={12} sm={10}>
              <Paper sx={{ width: '100%', overflow: 'hidden' }}>
                <TableContainer>
                  <Table stickyHeader aria-label="sticky table">
                    <TableHead>
                      <TableRow>
                        {["First Name", "Last Name", "TZ", "Start Date", "Actions"].map((e) => {
                          return (<TableCell key={e} sx={{ fontWeight: 'bold', fontSize: '1.2rem', backgroundColor: '#2196f3', color: '#ffffff' }}>{e}</TableCell>)
                        })}
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {filteredEmployees.map((employee) => (
                        <TableRow key={employee.id} hover role="checkbox" tabIndex={-1}>
                          <TableCell>{employee.firstName}</TableCell>
                          <TableCell>{employee.lastName}</TableCell>
                          <TableCell>{employee.tz}</TableCell>
                          <TableCell>{employee.startDate ? convertToJavaScriptDate(employee.startDate).toLocaleDateString() : ""}</TableCell>
                          <TableCell>
                            <Button onClick={() => editForm(employee.tz)}><FontAwesomeIcon icon={faEdit} /></Button>
                            <Button onClick={() => navigate(`/deleteEmployee`, { state: employee.tz })} typeof="button"><FontAwesomeIcon icon={faTrash} /></Button>
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              </Paper>
            </Grid>
          </Grid>

        </Fragment>
      ) : <Grid
        container
        justifyContent="center"
        alignItems="center"
        style={{ height: "100vh" }}
      >
        <CircularProgress />
      </Grid>}
    </form>
  );
};
export default ShowEmployees;
