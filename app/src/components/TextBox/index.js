import React from 'react'
import './index.css'

const TextBox = ({id, name, placeholder, onChange, readonly}) => {
    return (
        <input type='text' id={id} name={name} placeholder={placeholder} onChange={onChange} autoComplete='off' readOnly={readonly} />
    );
}

export default TextBox