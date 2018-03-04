import React from "react";

class Result extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    let dealerId = this.props.data.dealer_id;
    let name = this.props.data.Name;
    let city = this.props.data.City;
    let state = this.props.data.State;
    return (<div className="col-md-4">
      <div className="card mb-4 box-shadow">
        <img className="card-img-top" src={`https://dealerontest.blob.core.windows.net/dealerscreencaps/${dealerId}/default.jpg`} width="300" alt="Card image cap"></img>
        <div className="card-body">
          <p className="card-text"><a href={`http://dealer${dealerId}.dealeron.com`}><strong>{name}</strong></a> &mdash; {city}, {state}</p>
          <div className="d-flex justify-content-between align-items-center">
            <div className="btn-group">
              <button type="button" className="btn btn-sm btn-outline-secondary">View</button>
            </div>
            <small className="text-muted">Dealer {dealerId}</small>
          </div>
        </div>
      </div>
    </div>);
  }
}

export default Result;