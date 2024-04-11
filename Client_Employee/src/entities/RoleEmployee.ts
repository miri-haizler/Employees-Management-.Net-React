import Employee from "./Employee";
import Role from "./Role";

export default interface RoleEmployee {
  id: number,
  roleId: number,
  roles: Role,
  employeeDetailsId: number,
  employees: Employee
  startDateOfJob: Date,
  isManagerial: boolean,
  isActive: boolean
}