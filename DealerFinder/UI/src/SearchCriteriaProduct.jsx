import React from "react";

class SearchCriteriaProduct extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (<div className="custom-control custom-checkbox">
      <input className="custom-control-input" id={this.props.type} value={this.props.type} type="checkbox" onChange={this.props.onChange}></input>
      <label className="custom-control-label" htmlFor={this.props.type}>{this.props.value}</label>
    </div>);
  }
}

export default SearchCriteriaProduct;