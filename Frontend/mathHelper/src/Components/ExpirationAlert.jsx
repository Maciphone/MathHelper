import React from "react";

const ExpirationAlert = ({ onClose, onNavigate }) => {
  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>A munkamenet lejárt</h2>
        <p>Kérjük, jelentkezz be újra!</p>
        <button className="modal-button" onClick={onClose}>
          OK
        </button>
        <button className="modal-button" onClick={onNavigate}>
          Bejelentkezés
        </button>
      </div>
    </div>
  );
};

export default ExpirationAlert;
