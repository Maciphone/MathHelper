import { createSlice } from "@reduxjs/toolkit";


export const authSlice = createSlice({
    name: 'auth',
    initialState: {
        tokenExpiration: null,
    },
    reducers: {
        setTokenExpiration: (state, action) => {
            state.tokenExpiration = action.payload;
        },
        clearTokenExpiration: (state) => {
            state.tokenExpiration = null;
        }
    }
});

export const { setTokenExpiration, clearTokenExpiration } = authSlice.actions;

export default authSlice.reducer;