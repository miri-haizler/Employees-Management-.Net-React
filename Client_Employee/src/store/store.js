

import { createStore} from "redux";
// import thunk from 'redux-thunk'; // Correct import statement for thunk middleware
import reducer from "./reducer";

const store = createStore(reducer);

export default store;