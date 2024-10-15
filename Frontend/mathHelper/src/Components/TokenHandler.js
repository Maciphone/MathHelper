
import { jwtDecode } from "jwt-decode";


const TokenHandler = (token) => {

    try {
        const decoded = jwtDecode(token);
        const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        const nameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        const emailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        const userIDClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        console.log(decoded);
        console.log(decoded[roleClaim]);
        const claims = {
            email: decoded[emailClaim],
            name: decoded[nameClaim],
            role: decoded[roleClaim],
            userId: decoded[userIDClaim],
        }
        console.log(claims)
        return claims;
    } catch (error) {
        console.error("Failed to decode token", error);
        return null;
    }
};

export default TokenHandler;