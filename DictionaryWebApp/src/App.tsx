import { Container } from 'react-bootstrap';
import './App.css';
import DictionarySearch from './DictionarySearch/DictionarySearch';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Container>
          <DictionarySearch />
        </Container>
      </header>
    </div>
  );
}

export default App;
