import React, { useEffect, useState } from 'react';
import TextBox from '../TextBox';
import Button from '../Button';
import './index.css';
import {API_URL} from '../../config/config';

const Form = () => {
    const [original, setOriginal] = useState('');
    const [short, setShort] = useState('');
    const [message, setMessage] = useState('');

    useEffect(()=>{
        console.log(original);
    }, [original]);


    useEffect(()=>{
        console.log(short);
    }, [short]);

    const handleSubmit = (event) => {
        event.preventDefault();

        console.log('Calling api');

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type' : 'application/json'},
            body: JSON.stringify({url : original})
        };

        fetch(API_URL, requestOptions)
        .then(response => response.json())
        .then(data => {
            setShort(`${API_URL}/${data.short_url}`);
        })
        .catch(error => {
            setMessage(`It was not possible to perform this operation on ${API_URL}`);
            console.error('There was an error!', error);
        });

        console.log('Call ended');
    }

    return (
            <form onSubmit={handleSubmit}>
                <div name="message" className="message"><span>{message}</span></div>
                <TextBox  name='original' placeholder='Shorten your link' value={original} onChange={e=>{setOriginal(e.target.value)}} />
                <TextBox  name='short' placeholder='Shortened' value={short} onChange={e=>{setShort(e.target.value)}} disabled />
                <Button id='shorten' type='submit' />
            </form>
    )
}

export default Form;