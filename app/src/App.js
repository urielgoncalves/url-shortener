import './App.css';
import Title from './components/Title';
import Form from './components/Form';

const App = () => {
  return (
    <main>
      <div className="App">
        <header className="App-header">
          <Title>W.ly</Title>
        </header>
        
          <Form />
      </div>
    </main>
  );
}

export default App;
