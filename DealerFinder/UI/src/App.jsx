import React from "react";
import ReactDOM from "react-dom";
import "bootstrap/dist/css/bootstrap.min.css";

const App = () => {
  return (
    <div>
      <p>React here!</p>
    </div>
  );
};

export default App;
ReactDOM.render(<App />, document.getElementById("app"));