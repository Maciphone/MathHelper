
import { configureStore } from '@reduxjs/toolkit'


//export default userInformation.reducer; <a reducereket exportálom
import userInfoReducer from './userInformation';
import tokenReducer from './authSlice';


export default configureStore({
    reducer: {
        userData: userInfoReducer,
        authData: tokenReducer,
    },

})