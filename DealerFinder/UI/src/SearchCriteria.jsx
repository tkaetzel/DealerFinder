import React from "react";
import SearchCriteriaCategory from "./SearchCriteriaCategory.jsx";
import Result from "./Result.jsx";

class SearchCriteria extends React.Component {
  constructor(props) {
    super(props);
    this.state = { criteriaLoading: true, selected: [], results: [] };
  }

  componentDidMount() {
    fetch("/api/values").then(result => {
      return result.json();
    }).then(resultJson => {
      this.setState({ criteria: resultJson, criteriaLoading: false });
    });
  }

  handleChange(evt) {
    let clickedItem = evt.target.id;
    let currentSelected = this.state.selected;
    if (evt.target.checked) {
      currentSelected.push(clickedItem);
    } else {
      let index = currentSelected.findIndex(a => a === clickedItem);
      if (index > -1) {
        currentSelected.splice(index, 1);
      }
    }

    this.setState({ selected: currentSelected });
  }

  doSearch() {
    this.setState({ resultLoading: true });
    fetch("/api/values",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(this.state.selected)
      }).then(result => {
        return result.json();
      }).then(resultJson => {
        this.setState({ results: resultJson, resultLoading: false });
      });
  }

  render() {
    let criteriaLoadingIcon = !this.state.criteriaLoading ? null : <img src="img/loading.gif" alt="Loading..."></img>;
    let searchCriteria = this.state.criteriaLoading ? null : (<div>
      <div className="row">
        <div className="col-md-6">
          <SearchCriteriaCategory name="Customizations" products={this.state.criteria.Customizations} onChange={e => this.handleChange(e)} />
        </div>
        <div className="col-md-6">
          <SearchCriteriaCategory name="Chat Providers" products={this.state.criteria.ChatProviders} onChange={e => this.handleChange(e)} />
        </div>
      </div>
      <div className="row">
        <div className="col-md-4">
          <SearchCriteriaCategory name="VDP Types" products={this.state.criteria.VdpTypes} onChange={e => this.handleChange(e)} />
        </div>
        <div className="col-md-4">
          <SearchCriteriaCategory name="Third Party Integrations" products={this.state.criteria.Tpis} onChange={e => this.handleChange(e)} />
        </div>
        <div className="col-md-4">
          <SearchCriteriaCategory name="Widgets" products={this.state.criteria.Widgets} onChange={e => this.handleChange(e)} />
        </div>
      </div>
    </div>);
    let results = this.state.results.map(r => <Result key={r.dealer_id} data={r} />);
    return (
      <div>
        <section className="jumbotron text-center">
          <div className="container">
            <h1 className="jumbotron-heading">Find a Dealer Now</h1>
            <p className="lead text-muted">Select your search criteria below.</p>
            {criteriaLoadingIcon}
            {searchCriteria}
            <p>
              <a href="#" className="btn btn-primary my-2" onClick={e => this.doSearch(e)}>Search</a>
              <a href="#" className="btn btn-secondary my-2">Reset</a>
            </p>
          </div>
        </section>

        <div className="album py-5 bg-light">
          <div className="container">
            <div className="row">
              {results}
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default SearchCriteria;