import { Api } from "#dictionaryApi";
import React from "react";
import Form from "react-bootstrap/Form";

interface ILanguageSelectorProps {
    excludedLanguage?: string;
    onChanged: (value: string) => void;
    value: string;
}

interface ILanguageSelectorState {
    languages: string[];
}

export default class LanguageSelector extends React.Component<ILanguageSelectorProps, ILanguageSelectorState> {

    private apiClient = new Api().api;

    constructor(props: ILanguageSelectorProps) {
        super(props);
        this.state = {
            languages: []
        };
    }

    private loadAsync = async () => {
        var languages = await this.apiClient.localizationGetLanguages();
        this.setState({languages: languages.data});
    }

    async componentDidMount() {
        await this.loadAsync();
    }

    private onChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        this.props.onChanged(e.target.value);
    }

    componentDidUpdate() {
        if(this.props.value === this.props.excludedLanguage) {
            this.props.onChanged(this.state.languages.find(i => i !== this.props.excludedLanguage)!);
        }
    }

    public render() {

        var languages = this.props.excludedLanguage ? this.state.languages.filter(l => l !== this.props.excludedLanguage) : this.state.languages;

        return (
            <Form.Select className="m-1" value={this.props.value} onChange={this.onChange}>
                {languages.map((i, key) => <option value={i} key={key}>{i}</option>)}
            </Form.Select>
        );
    }
}