import React, { useEffect, useState } from 'react';
import TextBox from '../TextBox';
import Button from '../Button';
import './index.css';

const Form = () => {
    const [original, setOriginal] = useState('');
    const [short, setShort] = useState('');
    const API_URL = 'https://localhost:5001';

    const handleSubmit = (event) =>{
        event.preventDefault();

        console.log('Calling api');

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type' : 'application/json'},
            body: JSON.stringify({url : original})
        };

        fetch(API_URL + '/shortener', requestOptions)
        .then(response => response.json())
        .then(data => {
            console.log(data);
            setShort(data.short_url); //TODO: Not working
        });

        console.log('Call ended');
    }

    return (
            <form onSubmit={handleSubmit}>
                <TextBox id='idoriginal' name='original' placeholder='Shorten your link' value={original} onChange={e=>{setOriginal(e.target.value)}} />
                <TextBox id='idshort' name='short' placeholder='Shortened' value={short} onChange={e=>{setShort(e.target.value)}} />
                <Button id='shorten' type='submit' />
            </form>
    )
}

export default Form;