

const keyString = import.meta.env.VITE_AES_KEY;
const ivString = import.meta.env.VITE_AES_IV;

const keyBytes = new TextEncoder().encode(keyString);
const ivBytes = new TextEncoder().encode(ivString);


export const decryptData = async (encryptedBase64) => {
    try {
        // Base64 dekódolás

        const encryptedBytes = Uint8Array.from(atob(encryptedBase64), c =>
            c.charCodeAt(0)
        );

        // Kulcs importálása a Web Crypto API-ba
        const cryptoKey = await window.crypto.subtle.importKey(
            "raw",
            keyBytes,
            { name: "AES-CBC" },
            false,
            ["decrypt"]
        );

        // Visszafejtés
        const decryptedBuffer = await window.crypto.subtle.decrypt(
            { name: "AES-CBC", iv: ivBytes },
            cryptoKey,
            encryptedBytes
        );

        // Byte tömb stringgé alakítása
        var resultString = new TextDecoder().decode(decryptedBuffer);
        console.log("webCryptoban: ", resultString)
        return parseInt(resultString);
    } catch (error) {
        console.error("Decryption failed", error);
        return null;
    }
}