
import { configureStore } from '@reduxjs/toolkit'


//export default userInformation.reducer; <a reducereket exportálom
import userInfoReducer from './userInformation'




export default configureStore({
    reducer: {
        userData: userInfoReducer,
    },
})