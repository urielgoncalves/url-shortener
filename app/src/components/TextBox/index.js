import React from 'react';
import './index.css';

const TextBox = ({id, name, placeholder, onChange, readonly, value, disabled}) => {
    return (
        <input type='text' id={id} name={name} placeholder={placeholder} value={value} onChange={onChange} autoComplete='off' readOnly={readonly} disabled={disabled} />
    );
}

export default TextBox;