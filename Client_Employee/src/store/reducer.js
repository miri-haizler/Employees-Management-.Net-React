const initalState = {
    employees: []
}
const reducer = (state = initalState, action) => {

    switch (action.type) {

        case "GET_EMPLOYEES":
            {
                return { ...state, employees: action.data }
            }

        case "DELETE_EMPLOYEE":
            {
                console.log("action data of delete",action.data);
                return { ...state, employees: action.data }
                // return { 
                //     ...state, 
                //     employees: state.employees.filter(employee => employee.id !== action.data.id) 
                // };
            }
            // case "DELETE_PRODUCT":
            //     {
            //         const id = action.data;
            //         console.log(id)
            //         const filtered = state.recipes.filter((recipe) => recipe.Id != id)
            //         state.buyies = filtered;
            //         return {
            //             ...state,
            //             filtered
            //         }
            //     }

        default: return { ...state }
    }

}
export default reducer;
