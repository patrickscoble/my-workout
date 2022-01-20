import React, { useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

export const CreateProgram = (props) => {

  const [name, setName] = useState('');

  useEffect(() => {
    if (!props.program.id) {
      setName('');
      return;
    }

    setName(props.program.name);
  }, [props.program]);

  const saveAndCloseCreateProgramModal = () => {
    props.program.name = name;

    props.saveAndCloseCreateProgramModal();
  }

  return (
    <Modal show={props.showCreateProgramModal} onHide={props.closeCreateProgramModal} dialogClassName="create-program-modal">
      <Modal.Header>
        <Modal.Title>{props.program.id ? "Update Program" : "Create Program"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="container">
          <form>
            <div className="row">
              <input type="text" onChange={(e) => setName(e.target.value)} className="form-control" name="name" id="name" placeholder="Name" value={name} maxLength="50" />
            </div>
          </form>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.closeCreateProgramModal}>
          Cancel
        </Button>
        <Button variant="primary" onClick={saveAndCloseCreateProgramModal} disabled={!name}>
          {props.program.id ? 'Update' : 'Create'}
        </Button>
      </Modal.Footer>
    </Modal>  
  )
}
