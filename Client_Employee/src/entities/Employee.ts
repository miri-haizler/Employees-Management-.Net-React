import RoleEmployee from "./RoleEmployee";

export type Gender= 'Male' | 'Female'

export default interface Employee {
    id: number,
    firstName: string,
    lastName: string,
    tz: string,
    startDate:Date,
    dateOfBirth: Date,
    gender: Gender,
    isActive: Boolean,
    rolesEmployee: RoleEmployee[]
}