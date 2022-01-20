import React, { useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';

export const CreateProgramExercise = (props) => {

  const [name, setName] = useState('');
  const [sets, setSets] = useState('');
  const [repetitions, setRepetitions] = useState('');
  const [restPeriod, setRestPeriod] = useState('');

  useEffect(() => {
    if (!props.programExercise.id) {
      setName('');
      setSets('');
      setRepetitions('');
      setRestPeriod('');
      return;
    }

    setName(props.programExercise.name);
    setSets(props.programExercise.sets);
    setRepetitions(props.programExercise.repetitions);
    setRestPeriod(props.programExercise.restPeriod);
  }, [props.programExercise]);

  const saveAndCloseCreateProgramExerciseModal = () => {
    props.programExercise.name = name;
    props.programExercise.sets = sets;
    props.programExercise.repetitions = repetitions;
    props.programExercise.restPeriod = restPeriod;

    props.saveAndCloseCreateProgramExerciseModal();
  }

  return (
    <Modal show={props.showCreateProgramExerciseModal} onHide={props.closeCreateProgramExerciseModal} dialogClassName="create-program-exercise-modal">
      <Modal.Header>
        <Modal.Title>{props.programExercise.id ? "Update Exercise" : "Add Exercise"}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="container">
          <form>
            <div className="row form-group col-md-6">
              <input type="text" onChange={(e) => setName(e.target.value)} className="form-control" name="name" id="name" placeholder="Name" value={name} maxLength="50" />
              </div>
            <div className="row form-group col-md-6">
              <input type="number" onChange={(e) => setSets(e.target.value)} className="form-control" name="sets" id="sets" placeholder="Sets" value={sets} />
            </div>
            <div className="row form-group col-md-6">
              <input type="text" onChange={(e) => setRepetitions(e.target.value)} className="form-control" name="repetitions" id="repetitions" placeholder="Repetitions" value={repetitions} maxLength="25" />
            </div>
            <div className="row form-group col-md-6">
              <input type="text" onChange={(e) => setRestPeriod(e.target.value)} className="form-control" name="restPeriod" id="restPeriod" placeholder="Rest Period" value={restPeriod} maxLength="25" />
            </div>
          </form>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.closeCreateProgramExerciseModal}>
          Cancel
        </Button>
        <Button variant="primary" onClick={saveAndCloseCreateProgramExerciseModal} disabled={!name || !sets || !repetitions || !restPeriod}>
          {props.programExercise.id ? 'Update' : 'Add'}
        </Button>
      </Modal.Footer>
    </Modal>  
  )
}
