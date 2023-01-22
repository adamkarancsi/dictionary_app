import { Api } from "#dictionaryApi";
import React from "react";
import debounce from "lodash/debounce";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Form from 'react-bootstrap/Form';
import ListGroup from 'react-bootstrap/ListGroup';
import Overlay from 'react-bootstrap/Overlay';
import LanguageSelector from "../LanguageSelector/LanguageSelector";

interface IDictionarySearchProps {

}

interface IDictionarySearchState {
    value: string;
    result: string;
    autoCompleteItems: string[];
    showAutoComplete: boolean;

    sourceLanguage: string;
    targetLanguage: string;
}

export default class DictionarySearch extends React.Component<IDictionarySearchProps, IDictionarySearchState> {

    private client = new Api().api;
    private debounce_delay = 250;
    private overlayTarget: React.RefObject<HTMLInputElement>;

    constructor(props: IDictionarySearchProps) {
        super(props);

        this.overlayTarget = React.createRef();

        this.state = {
            value: "",
            result: "",
            autoCompleteItems: [],
            showAutoComplete: false,
            sourceLanguage: "English",
            targetLanguage: "Hungarian"
        };
    }

    private setResult = (result: string) => {
        this.setState({ result: result });
    };

    private setAutoCompleteOptions = (result: string[]) => {
        this.setState({ autoCompleteItems: result });
    }

    private showAutoComplete = () => {
        this.setState({ showAutoComplete: true });
    }

    private hideAutoComplete = () => {
        this.setState({ showAutoComplete: false });
    }

    private setSourceLanguage = (language: string) => {
        this.setState({sourceLanguage: language});
        this.getExactTranslationDebounced(this.state.value);
    }

    private setTargetLanguage = (language: string) => {
        this.setState({targetLanguage: language});
        this.getExactTranslationDebounced(this.state.value);
    }

    private searchAutoCompleteAsync = async (searchValue: string) => {
        var results = await this.client.localizationGetPossible({ language: this.state.sourceLanguage, searchValue: searchValue });
        return results.data;
    }

    private getExactTranslationAsync = async (searchValue: string) => {
        var result = await this.client.localizationGetExact({ searchValue: searchValue, sourceLanguage: this.state.sourceLanguage, targetLanguage: this.state.targetLanguage });
        return result.data;
    }

    private searchAutoCompleteDebounced = debounce(async (searchValue: string) => {
        if (searchValue) {
            var result = await this.searchAutoCompleteAsync(searchValue);
            this.setAutoCompleteOptions(result || []);
        } else {
            this.setAutoCompleteOptions([]);
        }
    }, this.debounce_delay);

    private getExactTranslationDebounced = debounce(async (searchValue) => {
        if (searchValue) {
            var result = await this.getExactTranslationAsync(searchValue);
            this.setResult(result || "");
        } else {
            this.setResult("");
        }
    }, this.debounce_delay);

    private onSearchChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({
            ...this.state,
            value: e.target.value
        });
        this.getExactTranslationDebounced(e.target.value);
        this.searchAutoCompleteDebounced(e.target.value);
        this.showAutoComplete();
    }

    private onAutoCompleteClickedFactory(value: string) {
        return () => {
            this.setState({ value: value });
            this.getExactTranslationDebounced(value);
            this.hideAutoComplete();
        };
    }

    private renderAutoCompleteItems(autoCompleteItems: string[]) {
        if (!autoCompleteItems || !this.state.showAutoComplete) {
            return null;
        } else {
            return (
                <ListGroup>
                    {autoCompleteItems.map((i, key) => <ListGroup.Item key={key} onClick={this.onAutoCompleteClickedFactory(i)}>{i}</ListGroup.Item>)}
                </ListGroup>
            )
        }
    }

    public render() {

        const { value, result, autoCompleteItems, showAutoComplete } = this.state;

        return (
            <Form>
                <Row>
                    <Col>
                        <Form.Group>
                            <Form.Label>Phrase to translate:</Form.Label>
                            <LanguageSelector value={this.state.sourceLanguage} onChanged={this.setSourceLanguage} />
                            <Form.Control className="m-1" onChange={this.onSearchChange} value={value} ref={this.overlayTarget} />
                            <Overlay target={this.overlayTarget.current} placement="bottom" show={showAutoComplete}>
                                {({ placement, arrowProps, show: _show, popper, ...props }) => (
                                    <div {...props}>
                                        {this.renderAutoCompleteItems(autoCompleteItems)}
                                    </div>
                                )}
                            </Overlay>
                        </Form.Group>
                    </Col>
                    <Col>
                        <Form.Group>
                            <Form.Label>Translated phrase:</Form.Label>
                            <LanguageSelector value={this.state.targetLanguage} onChanged={this.setTargetLanguage} excludedLanguage={this.state.sourceLanguage} />
                            <Form.Control className="m-1" value={result} disabled placeholder="Translation not found." />
                        </Form.Group>
                    </Col>
                </Row>
            </Form>
        );
    }
}