import logo from './logo.svg';
import './App.css';
import { useState, useEffect } from 'react';

function App() {
  const [players, setPlayers] = useState({});

  useEffect(() => {
    fetch('https://jotz04zyua.execute-api.eu-north-1.amazonaws.com/dev/scramble',{
      body: {
        teamSize: 5
      }
    })
    // const fetchData = async () => {
    //   const result = await fetch('https://jotz04zyua.execute-api.eu-north-1.amazonaws.com/dev/scramble',{
    //     body: 5
    //   });

    //   setPlayers(result.data);
    // };

    // fetchData();
  },[]);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        {
          players && players.map(item => 
            <li key={item.name}>{item.name}</li>
          )
        }
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
