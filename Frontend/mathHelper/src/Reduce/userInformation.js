import { createSlice } from "@reduxjs/toolkit";

export const userInformation = createSlice({
    name: 'userName',
    initialState: {
        value: null,
    },
    reducers: {
        addName: (state, action) => {
            state.value = action.payload;
        },
        removeName: (state) => {
            state.value = null;
        }
    }
});

export const { addName, removeName } = userInformation.actions;

export default userInformation.reducer;
