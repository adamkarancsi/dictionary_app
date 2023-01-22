import { Api } from "#dictionaryApi";
import React from "react";
import debounce from "lodash/debounce";
import { Container, Row, Col } from "react-bootstrap";
import Form from 'react-bootstrap/Form';
import ListGroup from 'react-bootstrap/ListGroup';
import Overlay from 'react-bootstrap/Overlay';

interface IDictionarySearchProps {

}

interface IDictionarySearchState {
    value: string;
    result: string;
    autoCompleteItems: string[];
    showAutoComplete: boolean;
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
            showAutoComplete: false
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

    private searchAutoCompleteAsync = async (searchValue: string) => {
        var results = await this.client.localizationGetPossible({ language: "English", searchValue: searchValue });
        return results.data;
    }

    private getExactTranslationAsync = async (searchValue: string) => {
        var result = await this.client.localizationGetExact({ searchValue: searchValue, sourceLanguage: "English", targetLanguage: "Hungarian" });
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
            <Container>
                <Row>
                    <Col>
                        <Form.Group>
                            <Form.Label>Phrase to translate:</Form.Label>
                            <Form.Control onChange={this.onSearchChange} value={value} ref={this.overlayTarget} />
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
                            <Form.Control value={result} disabled />
                        </Form.Group>
                    </Col>
                </Row>
            </Container>
        );
    }
}