import moment from 'moment';
import React, { useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { getAllWorkoutExercisesByWorkoutIdAsync, updateWorkoutExerciseAsync } from '../services/WorkoutExerciseService';

export const ViewWorkout = (props) => {

  const [workoutExercises, setWorkoutExercises] = useState([]);

  useEffect(() => {
    if (!props.workout.id) return;

    getAllWorkoutExercisesByWorkoutIdAsync(props.workout.id)
      .then(workoutExercises => setWorkoutExercises(workoutExercises));

    return function cleanup() {
      setWorkoutExercises([]);
    };

  }, [props.workout]);

  const saveAndCloseViewWorkoutModal = () => {
    workoutExercises.forEach(workoutExercise => {
      workoutExercise.workoutId = workoutExercise.workout.id;
      workoutExercise.programExerciseId = workoutExercise.programExercise.id;
      updateWorkoutExerciseAsync(workoutExercise)
    });

    props.closeViewWorkoutModal();
  }

  const onChangeWorkoutExerciseForm = (e, id) => {
    let index = workoutExercises.findIndex(workoutExercise => workoutExercise.id === id);
    if (e.target.name === 'weight') {
      workoutExercises[index].weight = e.target.value;  
    }
    else if (e.target.name === 'maxedOut') {
      workoutExercises[index].maxedOut = JSON.parse(e.target.value); // Convert to a boolean.
    }
    setWorkoutExercises(workoutExercises);
  }

  const WorkoutExerciseRow = (workoutExercise, index) => {
    return (
      <tr key={index}>
        <td>{workoutExercise.programExercise.name}</td>
        <td>{workoutExercise.programExercise.sets}</td>
        <td>{workoutExercise.programExercise.repetitions}</td>
        <td>{workoutExercise.programExercise.restPeriod}</td>
        <td><input type="text" onChange={(e) => onChangeWorkoutExerciseForm(e, workoutExercise.id)} className="form-control" name="weight" id="weight" placeholder="Weight" defaultValue={workoutExercise.weight} maxLength="25" /></td>
        <td>
          <select onChange={(e) => props.onChangeWorkoutExerciseForm(e, workoutExercise.id)} name="maxedOut" id="maxedOut" defaultValue={workoutExercise.maxedOut}>
            <option value={true}>{`\u{2714}`} Yes</option>
            <option value={false}>{`\u{274C}`} No</option>
          </select>
        </td>
      </tr>
    )
  }

  const workoutExerciseTable = workoutExercises.map((workoutExercise, index) => WorkoutExerciseRow(workoutExercise, index))

  return (
    <Modal show={props.showViewWorkoutModal} onHide={props.closeViewWorkoutModal} dialogClassName="view-workout-modal">
      <Modal.Header>
        <Modal.Title>{moment(props.workout.date).format('DD/MM/YYYY')}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="container">
          <h4>Workout Exercises</h4>
          <table className="table">
            <thead className="table-header">
              <tr>
                <th>Name</th>
                <th>Sets</th>
                <th>Repetitions</th>
                <th>Rest Period</th>
                <th>Weight</th>
                <th>Maxed Out</th>
              </tr>
            </thead>
            <tbody>
              {workoutExerciseTable}
            </tbody>
          </table>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.closeViewWorkoutModal}>
          Cancel
        </Button>
        <Button variant="primary" onClick={saveAndCloseViewWorkoutModal}>
          Save
        </Button>
      </Modal.Footer>
    </Modal>
  )
}
