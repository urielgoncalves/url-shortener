import React from 'react';
import './index.css';

const Button = (props) => {
    return (
        <button id={props.id} type={props.type}>Shorten</button>
    );
}

export default Button;