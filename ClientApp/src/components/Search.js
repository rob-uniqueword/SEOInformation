import React, { Component } from 'react';

export class Search extends Component {
    static displayName = Search.name;

    constructor(props) {
        super(props);

        this.state = {
            searchProviders: ["Google"],
            searchProviderName: "Google",
            searchString: "land registry search",
            targetURL: "infotrack.co.uk",
            submitted: false,
            loading: false,
            seoResult: null
        };
    }

    componentDidMount() {
        this.populateSearchProviders();
    }

    async populateSearchProviders() {
        const response = await fetch('SearchProviders');
        const result = await response.json();
        this.setState({ searchProviders: result });
    }

    searchProviderNameChangeHandler = (event) => {
        this.setState({ searchProviderName: event.target.value });
    }

    searchStringChangeHandler = (event) => {
        this.setState({ searchString: event.target.value });
    }

    targetURLChangeHandler = (event) => {
        this.setState({ targetURL: event.target.value });
    }

    formSubmitHandler = (event) => {
        event.preventDefault();
        this.setState({ submitted: true, loading: true });
        this.populateSEOResult(this.state.searchProviderName, this.state.searchString, this.state.targetURL);
    }

    async populateSEOResult(searchProviderName, searchString, targetURL) {
        searchProviderName = encodeURIComponent(searchProviderName);
        searchString = encodeURIComponent(searchString);
        targetURL = encodeURIComponent(targetURL);

        const paramstring = `?searchProviderName=${searchProviderName}&searchString=${searchString}&targetURL=${targetURL}`

        const response = await fetch('SEOResult' + paramstring);

        const result = await response.json();

        this.setState({ seoResult: result, loading: false });
    }

    render() {
        return (
            <div>
                <form onSubmit={this.formSubmitHandler}>
                    <p>Search provider:</p>
                    <select defaultValue={this.state.searchProviderName} onChange={this.searchProviderNameChangeHandler}>
                        {this.state.searchProviders.map(provider => (<option key={provider} value={provider}>{provider}</option>))}
                    </select>
                    <p>Query:</p>
                    <input type='text' onChange={this.searchStringChangeHandler} defaultValue={this.state.searchString}/>
                    <p>Target URL:</p>
                    <input type='text' onChange={this.targetURLChangeHandler} defaultValue={this.state.targetURL} />
                    <p/>
                    <input type='submit'/>
                </form>
                <Results submitted={this.state.submitted} loading={this.state.loading} seoResult={this.state.seoResult} />
            </div>
        );
    }
}

function Results(props) {
    const submitted = props.submitted;
    const loading = props.loading;
    const seoResult = props.seoResult;

    if (!submitted) {
        return (
            <p></p>
        );
    }

    if (loading) {
        return (
            <p><em>Loading...</em></p>
        );
    }

    let occurrences;
    
    if (seoResult.occurrences.length) {
        occurrences = seoResult.occurrences.map(occurrence => <p>{occurrence}</p>);
    } else {
        occurrences = "N/A";
    }

    return (
        <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>SearchProvider</th>
                    <th>SearchString</th>
                    <th>TargetURL</th>
                    <th>Occurrences</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>{seoResult.searchProvider}</td>
                    <td>{seoResult.searchString}</td>
                    <td>{seoResult.targetURL}</td>
                    <td>{occurrences}</td>
                </tr>
            </tbody>
        </table>
    );
}