import React from "react";
import SearchCriteriaProduct from "./SearchCriteriaProduct.jsx";

class SearchCriteriaCategory extends React.Component {
  constructor(props) {
    super(props);
    this.state = { products: [] };
  }

  componentDidMount() {
    let products = this.props.products.products.map(p => <SearchCriteriaProduct type={p.type} key={p.type} value={p.value} onChange={this.props.onChange} />);
    this.setState({ products: products });
  }

  render() {
    return (
      <fieldset>
        <legend>{this.props.name}</legend>
        <div className="form-group">
          {this.state.products}
        </div>
      </fieldset>
    );
  }
}

export default SearchCriteriaCategory;